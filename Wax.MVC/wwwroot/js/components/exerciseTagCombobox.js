const exerciseTagEditDialog = {
    template: `
    <v-dialog v-model="show">
        <v-card title="Dialog">
            <v-card-text>
                <v-text-field v-model="tag.name"
                              label="標籤名稱"
                              required></v-text-field>

                <v-select v-model="tag.category" label="標籤類別" :items="categoryItems" item-title="key" ></v-select>
            </v-card-text>

            <v-card-actions>
                <v-btn text="確定" @click="submit()"></v-btn>
                <v-btn text="取消" @click="show = false"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    `,
    data() {
        return {
            show: false,
            resolve: null,

            tag: {
                category: null,
                name: '',
            },
        }
    },
    props: {
        categoryItems: {
            type: Array,
            required: true,
        }
    },
    methods: {
        open(name) {
            this.tag.name = name
            this.tag.category = null
            let self = this
            return new Promise((resolve, reject) => {
                self.resolve = resolve
                self.show = true
            })
        },
        submit() {
            if (this.tag.name == null || this.tag.name.length < 1) {
                alertHelper.alert("請輸入標籤名稱")
                return
            }

            if (!this.categoryItems.some(x => x.value == this.tag.category)) {
                this.tag.category = null
                alertHelper.alert("請輸入標籤類別")
                return
            }

            this.show = false;
            this.resolve(Object.assign({}, this.tag))
        }
    },
    watch: {
        show() {
            if (!this.show) {
                this.resolve(false)
            }
        }
    },
}

const exerciseTagCombobox = {
    template: `
    <v-combobox v-model="selected"
                v-model:search="search"
                :hide-no-data="false"
                :items="exerciseTagItems"
                item-title="name"
                @keydown.enter.prevent="addingNewTag"
                multiple
                hide-selected
                persistent-hint>
        <template v-slot:selection="{item, index}">
            <v-chip
                :key="JSON.stringify(item.raw)"
                :color="getChipColor(item.raw.category)">
                {{ item.title }}
            </v-chip>
        </template>
        <template v-slot:no-data>
            <v-list-item @click="addingNewTag">
                <v-list-item-title>
                    點擊此列以新增 <kbd>{{ search }}</kbd>
                </v-list-item-title>
            </v-list-item>
        </template>
    </v-combobox>

    <v-exercise-tag-edit-dialog ref="tagEditDialog" :category-items="categoryItems"></v-exercise-tag-edit-dialog>
    `,
    components: {
        'v-exercise-tag-edit-dialog': exerciseTagEditDialog
    },
    data() {
        return {
            selected: [...this.modelValue],
            search: null,
        }
    },
    props: {
        modelValue: {
            type: Array,
            required: true,
        },
        exerciseTagItems: {
            type: Array,
            required: true,
        },
        categoryItems: {
            type: Array,
            required: true,
        }
    },
    methods: {
        async addingNewTag() {
            let search = this.search;
            var tag = await this.$refs.tagEditDialog.open(search)
            if (tag) {
                this.selected.push(tag)
            }
            this.selected = this.selected.filter(x => x != search)
        },
        getChipColor(category) {
            switch (category) {
                case 1:
                    return "deep-purple"
                case 2:
                    return "teal"
                case 3: 
                    return "indigo"
                default:
                    return "blue-grey"
            }
        }
    },
    watch: {
        selected() {
            this.$emit('update:modelValue', this.selected)
        },
        modelValue() {
            this.selected = this.modelValue
        }
    },
}